import 'dart:io';
import 'package:flutter/material.dart';
import 'package:smart_places/services/rest_api_client.dart';
import 'package:smart_places/widget/drawer.dart';

class SummaryPage extends StatefulWidget {
  SummaryPage({Key key}) : super(key: key);

  static const String routeName = '/summary';

  @override
  _SummaryPageState createState() => _SummaryPageState();
}

class _SummaryPageState extends State<SummaryPage> {
  bool value = false;
  final RestApiClient restApiClient =
      RestApiClient(httpClient: new HttpClient());

  @override
  Widget build(BuildContext context) {
    return new Scaffold(
      backgroundColor: value ? Colors.black : Colors.white,
      drawer: AppDrawer(),
      appBar: new AppBar(
        title: new Text('Özet Bilgiler'),
      ),
      body: Center(
        child: Switch(
            value: value,
            onChanged: (v) {
              restApiClient.getSettings();
              setState(() {
                value = v;
              });
            }),
      ),
    );
  }
}
