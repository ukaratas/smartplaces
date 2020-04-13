import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:smart_places/blocs/settings/settings_bloc.dart';
import 'package:smart_places/blocs/settings/settings_event.dart';
import 'package:smart_places/widgets/drawer/drawer_widget.dart';
import 'package:smart_places/widgets/virtual_layout/settings_full_layout_widget.dart';

class LightSwitchPage extends StatefulWidget {
  static const String routeName = '/light_switch';

  final regionId;
  
  LightSwitchPage({Key key, this.regionId}) : super(key: key);

  @override
  _LightSwitchPageState createState() => _LightSwitchPageState();
}

class _LightSwitchPageState extends State<LightSwitchPage> {
  @override
  Widget build(BuildContext context) {
    //final Map arguments = ModalRoute.of(context).settings.arguments as Map;
    // if (arguments != null) id = arguments['id'];

    return new Scaffold(
        backgroundColor: Colors.white,
        drawer: BlocProvider(
            create: (context) => SettingsBloc()..add(GetSettings()),
            child: DrawerWidget()),
        appBar: new AppBar(
          title: new Text('Aydınlatma ($widget.regionId)'),
        ),
        body: BlocProvider(
            create: (context) => SettingsBloc()..add(GetSettings()),
            child: SettingsFullLayoutWidget(id: widget.regionId)));
  }
}
