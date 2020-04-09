import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:smart_places/blocs/settings/settings_bloc.dart';
import 'package:smart_places/blocs/settings/settings_state.dart';
import 'package:smart_places/models/region.dart';
import 'package:smart_places/models/settings.dart';

class SettingsFullLayoutWidget extends StatefulWidget {
  SettingsFullLayoutWidget({Key key, this.id}) : super(key: key);

  final String id;

  @override
  _SettingsFullLayoutWidgetState createState() =>
      _SettingsFullLayoutWidgetState(id: id);
}

class _SettingsFullLayoutWidgetState extends State<SettingsFullLayoutWidget> {
  final String id;

  _SettingsFullLayoutWidgetState({this.id}) : super();

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
        return GridLoader(settings: state.settings, regionId: id);
      }

      return Center(
        child: Text('@ Not implemented state'),
      );
    });
  }
}

class GridLoader extends StatelessWidget {
  final Settings settings;
  final String regionId;

  GridLoader({this.settings, this.regionId});

  @override
  Widget build(BuildContext context) {
    return SingleChildScrollView(
        child: Column(children: <Widget>[
      Container(
        margin: EdgeInsets.all(10),
        child: Table(
          border: TableBorder.all(),
          children: _buildRows(context, settings, regionId),
        ),
      )
    ]));
  }

  List<TableRow> _buildRows(
      BuildContext context, Settings settings, String regionId) {
    Region region =
        settings.regions.where((region) => region.id == regionId).first;

    List<TableRow> rows = List<TableRow>();

    var areaW = MediaQuery.of(context).size.width *.9 ;

    for (var i = 0; i < region.layoutRowsCount; i++) {
      List<Column> columns = List<Column>();

      var rowRatio = region.rows.singleWhere((row) => row.no == i + 1);

      for (var j = 0; j < region.layoutColumnCount; j++) {
        columns.add(Column(children: [
          Container(height: rowRatio.percetage * areaW * region.aspectRatio / 100),
        ]));
      }

      rows.add(TableRow(children: columns));
    }

    region.sections.forEach((section) {
      section.gadgets.forEach((gadget) {
        if (gadget.typeGroup == "Relay") {
          var col =
              rows[section.row - 1].children[section.column - 1] as Column;
          col.children.add(Text(gadget.name));
          col.children.add(Switch(value :true, onChanged: (value)=>{}, ));
        }
      });
    });

    return rows;
  }
}

//List<Widget> GetColumnSections(Region region, int row, int column) {}

/*
    return GridView.count(
      primary: false,
      padding: const EdgeInsets.all(20),
      crossAxisSpacing: 10,
      mainAxisSpacing: 10,
      crossAxisCount: 5,
      children: <Widget>[
        Container(
          padding: const EdgeInsets.all(8),
          child: const Text('He\'d have you all unravel at the'),
          color: Colors.teal[100],
        ),
        Container(
          padding: const EdgeInsets.all(8),
          child: const Text('Heed not the rabble'),
          color: Colors.teal[200],
        ),
        Container(
          padding: const EdgeInsets.all(8),
          child: const Text('Sound of screams but the'),
          color: Colors.teal[300],
        ),
        Container(
          padding: const EdgeInsets.all(8),
          child: const Text('Who scream'),
          color: Colors.teal[400],
        ),
        Container(
          padding: const EdgeInsets.all(8),
          child: const Text('Revolution is coming...'),
          color: Colors.teal[500],
        ),
        Container(
          padding: const EdgeInsets.all(8),
          child: const Text('Revolution, they...'),
          color: Colors.teal[600],
        ),
      ],
    );
    */
