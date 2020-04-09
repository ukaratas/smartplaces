import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:smart_places/blocs/settings/settings_bloc.dart';
import 'package:smart_places/blocs/settings/settings_event.dart';
import 'package:smart_places/screens/settings/settings_full_layout_widget.dart';
import 'package:smart_places/widgets/drawer/drawer_widget.dart';

class LightSwitchPage extends StatefulWidget {
  LightSwitchPage({Key key}) : super(key: key);

  static const String routeName = '/light_switch';

  @override
  _LightSwitchState createState() => _LightSwitchState();
}

class _LightSwitchState extends State<LightSwitchPage> {
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
          title: new Text('Aydınlatma ($id)'),
        ),
        body: BlocProvider(
            create: (context) => SettingsBloc()..add(GetSettings()),
            child: SettingsFullLayoutWidget(id: id)));
  }
}
