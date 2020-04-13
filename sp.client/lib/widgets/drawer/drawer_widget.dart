import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_icons/flutter_icons.dart';
import 'package:font_awesome_flutter/font_awesome_flutter.dart';
import 'package:smart_places/blocs/settings/settings_bloc.dart';
import 'package:smart_places/blocs/settings/settings_state.dart';
import 'package:smart_places/models/settings.dart';
import '../../routes.dart';

class DrawerWidget extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Drawer(child:
        BlocBuilder<SettingsBloc, SettingsState>(builder: (context, state) {
      if (state is SettingsNotLoaded) {
        return Center(
          child: CircularProgressIndicator(),
        );
      }

      if (state is SettingsLoaded) {
        return _buildItems(context, state.settings);
      }

      return Center();
    }));
  }

  Widget _buildItems(BuildContext context, Settings settings) {
    return ListView(
        padding: EdgeInsets.zero,
        children: _createDrawerItems(context, settings));
  }

  List<Widget> _createDrawerItems(BuildContext context, Settings settings) {
    List<Widget> items = List<Widget>();

    items.add(_createHeader());
    items.add(_createDrawerItem(
        icon: Icons.home,
        text: 'Özet',
        onTap: () => Navigator.pushNamed(context, Routes.summary)));

    _createGroupMenuByGadgetType(
        context,
        settings,
        items,
        "SwitchForLight",
        "Aydınlatma Anahtarlar",
        FontAwesomeIcons.lightbulb,
        Routes.lightSwitch);

    _createGroupMenuByGadgetType(context, settings, items, "SwitchForDevice",
        "Diğer Anahtarlar", FontAwesomeIcons.plug, Routes.deviceSwitch);

    _createGroupMenuByGadgetType(context, settings, items, "RelayForOutlet",
        "Prizler", FontAwesomeIcons.toolbox, Routes.outlet);

    _createGroupMenuByGadgetType(context, settings, items, "RelayForDevice",
        "Cihazlar", FontAwesomeIcons.toggleOn, Routes.devices);

    _createGroupMenuByGadgetGroup(context, settings, items, "Sensor",
        "Sensörler", FontAwesomeIcons.thermometerHalf, Routes.sensor);

    _createTankGroupMenu(context, settings, items);

    // footer section
    items.add(Divider());

    items.add(_createDrawerItem(
        icon: Icons.settings,
        text: 'Ayarlar',
        onTap: () => Navigator.pushNamed(context, Routes.settings)));

    items.add(ListTile(
      title: Text('0.0.1'),
      onTap: () {},
    ));

    return items;
  }

  _createGroupMenuByGadgetType(
      BuildContext context,
      Settings settings,
      List<Widget> items,
      String gadgetType,
      String groupLabel,
      IconData icon,
      String route) {
    var itemCount = settings.numberOfRegionHasGadgetsByType(gadgetType);

    if (itemCount == 1) {
      settings.regions.forEach((region) {
        if (region.numberOfSectionHasGadgetsByType(gadgetType) > 0) {
          items.add(_createDrawerItem(
              icon: icon,
              text: groupLabel,
              onTap: () => Navigator.pushNamed(context, route,
                  arguments: {'regionId': region.id})));
        }
      });
    }

    if (itemCount > 1) {
      items.add(Row(children: <Widget>[
        Icon(icon),
        Padding(padding: EdgeInsets.all(4.0), child: Text(groupLabel))
      ]));

      settings.regions.forEach((region) {
        if (region.numberOfSectionHasGadgetsByType(gadgetType) > 0) {
          items.add(_createDrawerItem(
              text: region.name,
              onTap: () => Navigator.pushNamed(context, route,
                  arguments: {'regionId': region.id})));
        }
      });
    }
  }

  _createGroupMenuByGadgetGroup(
      BuildContext context,
      Settings settings,
      List<Widget> items,
      String gadgetGroup,
      String groupLabel,
      IconData icon,
      String route) {
    var itemCount = settings.numberOfRegionHasGadgetsByGroup(gadgetGroup);

    if (itemCount == 1) {
      settings.regions.forEach((region) {
        if (region.numberOfSectionHasGadgetsByGroup(gadgetGroup) > 0) {
          items.add(_createDrawerItem(
              icon: icon,
              text: groupLabel,
              onTap: () => Navigator.pushNamed(context, route,
                  arguments: {'regionId': region.id})));
        }
      });
    }

    if (itemCount > 1) {
      items.add(Row(children: <Widget>[
        Icon(icon),
        Padding(padding: EdgeInsets.all(4.0), child: Text(groupLabel))
      ]));

      settings.regions.forEach((region) {
        if (region.numberOfSectionHasGadgetsByGroup(gadgetGroup) > 0) {
          items.add(_createDrawerItem(
              text: region.name,
              onTap: () => Navigator.pushNamed(context, route,
                  arguments: {'regionId': region.id})));
        }
      });
    }
  }

  _createTankGroupMenu(
      BuildContext context, Settings settings, List<Widget> items) {
    items.add(Row(children: <Widget>[
      Icon(MaterialCommunityIcons.cup_water),
      Padding(padding: EdgeInsets.all(4.0), child: Text("Tanklar"))
    ]));

    settings.regions.forEach((region) {
      if (region.type == "Tank") {
        region.sections.forEach((section) {
          items.add(_createDrawerItem(
              text: section.name,
              onTap: () => Navigator.pushNamed(context, Routes.tankMonitor,
                  arguments: {'sectionId': section.id})));
        });
      }
    });
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
            padding: EdgeInsets.all(4.0), // only(left: 4.0, top: 1, bottom: 1),
            child: Text(text),
          )
        ],
      ),
      onTap: onTap,
    );
  }
}
