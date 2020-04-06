import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:smart_places/blocs/settings/settings_bloc.dart';
import 'package:smart_places/blocs/settings/settings_state.dart';

class SettingsFullLayoutWidget extends StatefulWidget {
  SettingsFullLayoutWidget({Key key}) : super(key: key);

  @override
  _SettingsFullLayoutWidgetState createState() =>
      _SettingsFullLayoutWidgetState();
}

class _SettingsFullLayoutWidgetState extends State<SettingsFullLayoutWidget> {
  /*
  SettingsBloc _settingsBloc;

  @override
  void initState() {
    super.initState();
    _settingsBloc = BlocProvider.of<SettingsBloc>(context);
  }
  */

  @override
  Widget build(BuildContext context) {
    return BlocBuilder<SettingsBloc, SettingsState>(builder: (context, state) {
      if (state is SettingsNotLoaded) {
        return Center(
          child: CircularProgressIndicator(),
        );
      }

      if (state is SettingsError) {
        return Center(
          child: Text('state == failed to fetch settings'),
        );
      }

      if (state is SettingsLoaded) {
        List<Widget> regionsWL = new List<Widget>();

        state.settings.regions.forEach((region) {
          regionsWL.add(new Text(region.name));

          List<Widget> sectionsWL = new List<Widget>();

          region.sections.forEach((section) {
            sectionsWL.add(Text('${section.name}'));
          });

          regionsWL.add(new Column(children: sectionsWL));
        });

        return Row(children: regionsWL);

/*
        for (var i = 0; i < state.settings.regions.length; i++) {
          
         
          var sectionTable = Table();
          state.settings.regions[i].sections.forEach((section) {
            var  row = new TableRow();
            var column = new Column();
            column.children.add((Text('${section.name}')));
            row.children.add(column);
            sectionTable.children.add(row);
            //sectionTable.children.add(TableRow(children: [Text('${section.name}')]))
          
         // });

          list.add(new Text(state.settings.regions[i].name));
          list.add(sectionTable);
        }
        return Row(children: list);
        */

        /*
        Center(
          child: Text('${state.settings.regions.length}'),

        );
        */
      }

      return Center(
        child: Text('@ Not implemented state'),
      );

      /*
      ListView.builder(
            itemBuilder: (BuildContext context, int index) { 
              return Table();
            }             
      );
      
      */
    });
  }
}
