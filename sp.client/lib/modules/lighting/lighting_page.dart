import 'package:flutter/material.dart';
import 'package:smart_places/widget/drawer.dart';
import 'package:smart_places/widget/place_layout.dart';

class LightingPage extends StatefulWidget {
  LightingPage({Key key}) : super(key: key);

  static const String routeName = '/lighting';

  @override
  _LightingPageState createState() => _LightingPageState();
}

class _LightingPageState extends State<LightingPage> {
  bool value = false;

  @override
  Widget build(BuildContext context) {
    return new Scaffold(
      backgroundColor: value ? Colors.black : Colors.white,
      drawer: AppDrawer(),
      appBar: new AppBar(
        title: new Text('Aydınlatma'),
      ),
      body: PlaceLayout(
  
      ),
    );
  }
}
