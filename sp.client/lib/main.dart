import 'package:flutter/material.dart';
import 'package:smart_places/pages/device_switch_page.dart';
import 'package:smart_places/pages/devices_page.dart';
import 'package:smart_places/pages/light_switch_page.dart';
import 'package:smart_places/pages/outlet_page.dart';
import 'package:smart_places/pages/sersors_page.dart';
import 'package:smart_places/pages/settings_page.dart';
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
        Routes.lightSwitch: (context) => LightSwitchPage(),
        Routes.sensor: (context) => SensorsPage(),
        Routes.outlet: (context) => OutletPage(),
        Routes.deviceSwitch: (context) => DeviceSwitchPage(),
        Routes.devices: (context) => DevicesPage(),
      },
    );
  }
}
