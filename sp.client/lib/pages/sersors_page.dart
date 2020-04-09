import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:smart_places/blocs/settings/settings_bloc.dart';
import 'package:smart_places/blocs/settings/settings_event.dart';
import 'package:smart_places/widgets/drawer/drawer_widget.dart';

class SensorsPage extends StatefulWidget {
  SensorsPage({Key key}) : super(key: key);

  static const String routeName = '/sensor';

  @override
  _SensorsPageState createState() => _SensorsPageState();
}

class _SensorsPageState extends State<SensorsPage> {
  String id;

  @override
  Widget build(BuildContext context) {
    final Map arguments = ModalRoute.of(context).settings.arguments as Map;
    if (arguments != null) id = arguments['id'];

    return new Scaffold(
        backgroundColor: Colors.white,
        drawer: BlocProvider(
            create: (context) => SettingsBloc()..add(GetSettings()),
            child: DrawerWidget()),
        appBar: new AppBar(
          title: new Text('Sensorler'),
        ),
         body: Center(child: Text('Sensors Region Id : $id')));
  }
}
