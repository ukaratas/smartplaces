import 'package:smart_places/pages/device_switch_page.dart';
import 'package:smart_places/pages/devices_page.dart';
import 'package:smart_places/pages/light_switch_page.dart';
import 'package:smart_places/pages/outlet_page.dart';
import 'package:smart_places/pages/sersors_page.dart';
import 'package:smart_places/pages/settings_page.dart';
import 'package:smart_places/pages/summary_page.dart';
import 'package:smart_places/pages/tank_monitor_page.dart';

class Routes {
  static const String summary = SummaryPage.routeName;
  static const String settings = SettingsPage.routeName;
  static const String lightSwitch = LightSwitchPage.routeName;
  static const String tankMonitor = TankMonitorPage.routeName;
  static const String sensor = SensorsPage.routeName;
  static const String outlet = OutletPage.routeName;
  static const String deviceSwitch = DeviceSwitchPage.routeName;
  static const String devices = DevicesPage.routeName;
}
