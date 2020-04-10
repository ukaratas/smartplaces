import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:smart_places/blocs/settings/settings_bloc.dart';
import 'package:smart_places/blocs/settings/settings_event.dart';
import 'package:smart_places/widgets/drawer/drawer_widget.dart';

class TankMonitorPage extends StatefulWidget {
  TankMonitorPage({Key key}) : super(key: key);

  static const String routeName = '/tank_monitor';

  @override
  _TankMonitorPageState createState() => _TankMonitorPageState();
}

class _TankMonitorPageState extends State<TankMonitorPage> {
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
          title: new Text('Tank Izleme Bilgiler'),
        ),
        body: Center(child: Text('Tank Section Id : $id')));
  }
}
