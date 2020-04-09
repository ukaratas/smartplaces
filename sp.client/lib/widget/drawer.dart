import 'package:flutter/material.dart';
import 'package:flutter_icons/flutter_icons.dart';
import 'package:font_awesome_flutter/font_awesome_flutter.dart';

import '../routes.dart';

class AppDrawer extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Drawer(
      child: ListView(
        padding: EdgeInsets.zero,
        children: <Widget>[
          _createHeader(),
          Text("subtitle"),
          _createDrawerItem(
              icon: Icons.home,
              text: 'Özet',
              onTap: () => Navigator.pushNamed(context, Routes.summary)),
          _createDrawerItem(
              icon: FontAwesomeIcons.lightbulb,
              text: 'Aydınlatma',
              onTap: () => Navigator.pushNamed(context, Routes.lighting)),
          _createDrawerItem(
              icon: FontAwesomeIcons.plug,
              text: 'Elektirik Dağıtım',
              onTap: () => Navigator.pop(context)),
          _createDrawerItem(
              icon: FontAwesomeIcons.sun,
              text: 'Kabin Isıtma',
              onTap: () => Navigator.pop(context)),
          _createDrawerItem(
              icon: FontAwesomeIcons.shower,
              text: 'Su Isıtma',
              onTap: () => Navigator.pop(context)),
          _createDrawerItem(
              icon: MaterialCommunityIcons.cup_water,
              text: 'Temiz Su Tankı',
              onTap: () => Navigator.pushNamed(context, Routes.cleanWater)),
          _createDrawerItem(
              icon: FontAwesomeIcons.database,
              text: 'Kirli Su Tankı',
              onTap: () => Navigator.pushNamed(context, Routes.cleanWater)),
          _createDrawerItem(
              icon: FontAwesomeIcons.burn,
              text: 'LPG Tankı',
              onTap: () => Navigator.pop(context)),
          _createDrawerItem(
              icon: FontAwesomeIcons.carBattery,
              text: 'Akü Yönetimi',
              onTap: () => Navigator.pop(context)),
          Divider(),
          _createDrawerItem(
              icon: Icons.settings,
              text: 'Ayarlar',
              onTap: () => Navigator.pop(context)),
          ListTile(
            title: Text('0.0.1'),
            onTap: () {},
          ),
        ],
      ),
    );
  }

  Widget _createHeader() {
    return UserAccountsDrawerHeader(
      accountEmail: Text("@ Home, Beykoz, Istanbul"),
      accountName: Text("Queen's Tardis"),
      currentAccountPicture: ClipRRect(
        borderRadius: BorderRadius.circular(110),
        child: Image.asset(
          "assets/images/van.png",
          fit: BoxFit.cover,
        ),
      ),
    );
  }

  Widget _createDrawerItem(
      {IconData icon, String text, GestureTapCallback onTap}) {
    return ListTile(
      title: Row(
        children: <Widget>[
          Icon(icon),
          Padding(
            padding: EdgeInsets.only(left: 8.0),
            child: Text(text),
          )
        ],
      ),
      onTap: onTap,
    );
  }
}
