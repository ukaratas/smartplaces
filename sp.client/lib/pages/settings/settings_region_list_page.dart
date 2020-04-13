import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:smart_places/blocs/settings/settings_bloc.dart';
import 'package:smart_places/blocs/settings/settings_event.dart';
import 'package:smart_places/models/modify_type.dart';
import 'package:smart_places/models/region.dart';
import 'package:smart_places/widgets/drawer/drawer_widget.dart';
import 'package:smart_places/widgets/settings/region_list_widget.dart';
import 'package:uuid/uuid.dart';

import '../../routes.dart';

class SettingsRegionListPage extends StatefulWidget {
  SettingsRegionListPage({Key key}) : super(key: key);

  static const String routeName = '/settings_region_list';

  @override
  _SettingsRegionListPageState createState() => _SettingsRegionListPageState();
}

class _SettingsRegionListPageState extends State<SettingsRegionListPage> {
  @override
  Widget build(BuildContext context) {
    return new Scaffold(
      backgroundColor: Colors.white,
      drawer: BlocProvider(
          create: (context) => SettingsBloc()..add(GetSettings()),
          child: DrawerWidget()),
      appBar: new AppBar(
        title: new Text('Bölgeler'),
      ),
      body: BlocProvider(
          create: (context) => SettingsBloc()..add(GetSettings()),
          child: RegionListWidget()),
      floatingActionButton: FloatingActionButton(
          onPressed: () => Navigator.pushNamed(
                  context, Routes.settingsRegionModify,
                  arguments: {
                    'region': Region(id: Uuid().v1(), aspectRatio: 0),
                    'modifyType': ModifyType.newItem
                  }),
          child: Icon(Icons.add)),
    );
  }
}
