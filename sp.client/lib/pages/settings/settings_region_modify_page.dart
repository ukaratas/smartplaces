import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:smart_places/blocs/settings/settings_bloc.dart';
import 'package:smart_places/blocs/settings/settings_event.dart';
import 'package:smart_places/models/modify_type.dart';
import 'package:smart_places/models/region.dart';
import 'package:smart_places/widgets/drawer/drawer_widget.dart';
import 'package:smart_places/widgets/settings/region_modify_widget.dart';

class SettingsRegionModifyPage extends StatefulWidget {
  static const String routeName = '/settings_region_modify';
  final Region region;
  final ModifyType modifyType;

  SettingsRegionModifyPage({Key key, this.region, this.modifyType})
      : super(key: key);

  @override
  _SettingsRegionModifyPageState createState() =>
      _SettingsRegionModifyPageState();
}

class _SettingsRegionModifyPageState extends State<SettingsRegionModifyPage> {

  @override
  Widget build(BuildContext context) {
    
    return new Scaffold(
        backgroundColor: Colors.white,
        drawer: BlocProvider(
            create: (context) => SettingsBloc()..add(GetSettings()),
            child: DrawerWidget()),
        appBar: new AppBar(
          title: new Text(widget.modifyType == ModifyType.updateExisting
              ? 'Bölge Güncelleme'
              : 'Yeni Bölge'),
        ),
        body: RegionModifyWidget(region: widget.region));
  }
}
