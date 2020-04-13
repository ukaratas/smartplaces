import 'package:flutter/material.dart';
import 'package:smart_places/pages/device_switch_page.dart';
import 'package:smart_places/pages/devices_page.dart';
import 'package:smart_places/pages/light_switch_page.dart';
import 'package:smart_places/pages/outlet_page.dart';
import 'package:smart_places/pages/sersors_page.dart';
import 'package:smart_places/pages/settings/settings_page.dart';
import 'package:smart_places/pages/settings/settings_region_list_page.dart';
import 'package:smart_places/pages/settings/settings_region_modify_page.dart';
import 'package:smart_places/pages/summary_page.dart';
import 'package:smart_places/pages/tank_monitor_page.dart';

import 'routes.dart';

void main() => {runApp(MyApp())};

class MyApp extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      color: Colors.orangeAccent,
      initialRoute: Routes.summary,
      routes: {
        Routes.summary: (context) => SummaryPage(),
        Routes.settings: (context) => SettingsPage(),
        Routes.lightSwitch: (context) => LightSwitchPage(),
        Routes.tankMonitor: (context) => TankMonitorPage(),
        Routes.lightSwitch: (context) {
          var regionId =
              (ModalRoute.of(context).settings.arguments as Map)['regionId'];
          return LightSwitchPage(key: Key(regionId), regionId: regionId);
        },
        Routes.sensor: (context) => SensorsPage(),
        Routes.outlet: (context) => OutletPage(),
        Routes.deviceSwitch: (context) => DeviceSwitchPage(),
        Routes.devices: (context) => DevicesPage(),
        Routes.settingsRegionList: (context) => SettingsRegionListPage(),
        Routes.settingsRegionModify: (context) {
          var region =
              (ModalRoute.of(context).settings.arguments as Map)['region'];
          var modifyType =
              (ModalRoute.of(context).settings.arguments as Map)['modifyType'];

          return SettingsRegionModifyPage(
              region: region, modifyType: modifyType);
        },
      },
    );
  }
}
