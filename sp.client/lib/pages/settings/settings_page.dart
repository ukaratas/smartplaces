import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:smart_places/blocs/settings/settings_bloc.dart';
import 'package:smart_places/blocs/settings/settings_event.dart';
import 'package:smart_places/widgets/drawer/drawer_widget.dart';

import '../../routes.dart';

class SettingsPage extends StatefulWidget {
  SettingsPage({Key key}) : super(key: key);

  static const String routeName = '/settings';

  @override
  _SettingsPageState createState() => _SettingsPageState();
}

class _SettingsPageState extends State<SettingsPage> {
  @override
  Widget build(BuildContext context) {
    return new Scaffold(
        backgroundColor: Colors.white,
        drawer: BlocProvider(
            create: (context) => SettingsBloc()..add(GetSettings()),
            child: DrawerWidget()),
        appBar: new AppBar(
          title: new Text('Ayarlar'),
        ),
        body: ButtonBar(
          children: <Widget>[
            FlatButton(
                child: Text('Bölgeler'),
                color: Colors.blue,
                onPressed: () =>
                    Navigator.pushNamed(context, Routes.settingsRegionList)),
          ],
        ));
  }
}
