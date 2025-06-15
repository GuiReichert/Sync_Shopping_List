import 'package:flutter/material.dart';
import 'dart:convert';
import 'package:http/http.dart' as http;

void main() {
  runApp(MyApp());
}

class Produto {
  int id;
  String nome;
  String categoria;
  bool selecionado;

  Produto(
      {required this.id,
      required this.nome,
      required this.categoria,
      required this.selecionado});

  factory Produto.fromJson(Map<String, dynamic> json) {
    return Produto(
      id: json['id'],
      nome: json['nome'],
      categoria: json['categoria'],
      selecionado: json['selecionado'],
    );
  }

  Map<String, dynamic> toJson() => {
        'id': id,
        'nome': nome,
        'categoria': categoria,
        'selecionado': selecionado,
      };
}

class MyApp extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      home: ListaDeCompras(),
      theme: ThemeData(
        primarySwatch: Colors.teal,
      ),
    );
  }
}

class ListaDeCompras extends StatefulWidget {
  @override
  _ListaDeComprasState createState() => _ListaDeComprasState();
}

class _ListaDeComprasState extends State<ListaDeCompras> {
  final String apiUrl =
      'http://192.168.15.10:7009/Produto'; // ajuste conforme necessário
  List<Produto> produtos = [];
  Map<String, bool> expandedCategorias = {};

  @override
  void initState() {
    super.initState();
    fetchProdutos();
  }

  Future<void> fetchProdutos() async {
    final response = await http.get(Uri.parse(apiUrl));
    if (response.statusCode == 200) {
      final List<dynamic> data = json.decode(response.body);
      setState(() {
        produtos = data.map((json) => Produto.fromJson(json)).toList();
        for (var p in produtos) {
          expandedCategorias[p.categoria] = false;
        }
      });
    }
  }

  Future<void> sincronizar() async {
    for (var produto in produtos) {
      await http.put(
        Uri.parse(apiUrl),
        headers: {'Content-Type': 'application/json'},
        body: json.encode(produto.toJson()),
      );
    }
    await http.get(Uri.parse('$apiUrl/Sync'));
  }

  Future<void> adicionarProduto(String nome, String categoria) async {
    final response = await http.post(
      Uri.parse(apiUrl),
      headers: {'Content-Type': 'application/json'},
      body: json.encode({
        'id': 0,
        'nome': nome,
        'categoria': categoria,
        'selecionado': false,
      }),
    );
    if (response.statusCode == 200) {
      fetchProdutos();
    }
  }

  Future<void> deletarProduto(int id) async {
    await http.delete(Uri.parse('$apiUrl/$id'));
    fetchProdutos();
  }

  void mostrarDialogoAdicionar() {
    String nome = '';
    String categoria = '';

    showDialog(
      context: context,
      builder: (_) => AlertDialog(
        title: Text('Adicionar Produto'),
        content: Column(
          mainAxisSize: MainAxisSize.min,
          children: [
            TextField(
              decoration: InputDecoration(labelText: 'Nome'),
              onChanged: (value) => nome = value,
            ),
            TextField(
              decoration: InputDecoration(labelText: 'Categoria'),
              onChanged: (value) => categoria = value,
            ),
          ],
        ),
        actions: [
          TextButton(
            onPressed: () {
              adicionarProduto(nome, categoria);
              Navigator.pop(context);
            },
            child: Text('Adicionar'),
          )
        ],
      ),
    );
  }

  @override
  Widget build(BuildContext context) {
    final categorias = produtos.map((e) => e.categoria).toSet().toList();
    return Scaffold(
      appBar: AppBar(
        title: Text('Lista de Compras'),
        actions: [
          IconButton(
            icon: Icon(Icons.sync),
            onPressed: sincronizar,
          ),
        ],
      ),
      body: ListView(
        children: categorias.map((categoria) {
          final produtosDaCategoria =
              produtos.where((p) => p.categoria == categoria).toList();
          return ExpansionTile(
            title: Text(categoria),
            initiallyExpanded: expandedCategorias[categoria] ?? false,
            onExpansionChanged: (expanded) {
              setState(() {
                expandedCategorias[categoria] = expanded;
              });
            },
            children: produtosDaCategoria.map((produto) {
              return ListTile(
                title: Text(produto.nome),
                leading: Checkbox(
                  value: produto.selecionado,
                  onChanged: (value) {
                    setState(() {
                      produto.selecionado = value!;
                    });
                  },
                ),
                trailing: IconButton(
                  icon: Icon(Icons.delete),
                  onPressed: () => deletarProduto(produto.id),
                ),
              );
            }).toList(),
          );
        }).toList(),
      ),
      floatingActionButton: FloatingActionButton(
        onPressed: mostrarDialogoAdicionar,
        child: Icon(Icons.add),
      ),
    );
  }
}
