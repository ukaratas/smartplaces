import 'package:flutter/material.dart';
import 'package:flutter_icons/flutter_icons.dart';
import 'package:font_awesome_flutter/font_awesome_flutter.dart';
import 'package:smart_places/blocs/settings/settings_bloc.dart';
import 'package:smart_places/blocs/settings/settings_event.dart';
import 'package:smart_places/models/modify_type.dart';
import 'package:smart_places/models/region.dart';
import 'package:smart_places/routes.dart';

class RegionWidget extends StatelessWidget {
  final Region region;

  const RegionWidget({Key key, @required this.region}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Card(
        child: Column(
      mainAxisSize: MainAxisSize.min,
      children: <Widget>[
        ListTile(
          leading: region.type == "Motohome"
              ? Icon(FontAwesomeIcons.caravan, size: 50, color: Colors.blue)
              : region.type == "Tank"
                  ? Icon(MaterialCommunityIcons.diving_scuba_tank_multiple,
                      size: 50, color: Colors.blue)
                  : Icon(FontAwesomeIcons.home, size: 50, color: Colors.blue),
          trailing: region.hasVirtualLayoutInfo
              ? Icon(MaterialCommunityIcons.layers_triple_outline,
                  size: 30, color: Colors.green)
              : Icon(MaterialCommunityIcons.layers_off_outline,
                  size: 30, color: Colors.red),
          title: Text(region.name),
          subtitle: Column(children: [
            if (region.hasVirtualLayoutInfo)
              ListTile(
                  title: Text("Arkaplan Resmi"),
                  subtitle: Text(region.backgroundImage.toString())),
            if (region.hasVirtualLayoutInfo)
              ListTile(
                  title: Text("Satır/Sütun Sayısı"),
                  subtitle: Text(region.layoutColumnCount.toString() +
                      '/' +
                      region.layoutRowsCount.toString())),
          ]),
        ),
        ButtonBar(
          alignment: MainAxisAlignment.spaceBetween,
          children: <Widget>[
            FlatButton(
              child: const Text('SİL'),
              onPressed: () {},
            ),
            FlatButton(
              child:
                  Text('BÖLÜMLER (' + region.sections.length.toString() + ')'),
              onPressed: () {},
            ),
            FlatButton(
              child: const Text('GÜNCELLE'),
              onPressed: () => Navigator.pushNamed(
                  context, Routes.settingsRegionModify,
                  arguments: {
                    'region': region.copy(),
                    'modifyType': ModifyType.updateExisting,
                    'onSave': (region) {
                      //region.update(region);

                      SettingsBloc()..add(UpdateRegion(region));

/*
                      BlocProvider(
                          create: (context) =>
                              SettingsBloc()..add(UpdateSettings()));
                              */
                    }
                  }),
            ),
          ],
        ),
      ],
    ));
  }
}
