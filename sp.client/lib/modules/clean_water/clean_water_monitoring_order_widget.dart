import 'package:flutter/material.dart';
import 'package:font_awesome_flutter/font_awesome_flutter.dart';
import 'package:intl/intl.dart';

import 'clean_water_monitoring_order.dart';

class CleanWaterMinitoringOrderWidget extends StatelessWidget {
  final CleanWaterMonitoringOrder item;
  //final dateFormatter = new DateFormat('hh:mm - d MMMM yyyy', 'tr');
  final dateFormatter = new DateFormat('hh:mm - d MMMM yyyy');

  CleanWaterMinitoringOrderWidget({Key key, @required this.item})
      : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Center(
        child: Card(
            child: Column(mainAxisSize: MainAxisSize.min, children: <Widget>[
      ListTile(
        leading: Icon(FontAwesomeIcons.hourglassStart),
        title: Text(dateFormatter.format(item.startDate)),
        isThreeLine: true,
        subtitle: Text('Başlangıç Zamanı'),
        dense: true,
      ),
      Visibility(
          visible: item.finishDate != null,
          child: ListTile(
            leading: Icon(FontAwesomeIcons.hourglassEnd),
            title: item.finishDate != null
                ? Text(dateFormatter.format(item.finishDate))
                : Text(''),
            isThreeLine: true,
            subtitle: Text('Tamamlanma Zamanı'),
            dense: true,
          )),
      ListTile(
        leading: Icon(FontAwesomeIcons.toiletPaper),
        title: Text(item.consumption.toString()),
        isThreeLine: true,
        subtitle: Text('Tüketim'),
        dense: true,
        trailing:
            item.finishDate == null ? Icon(FontAwesomeIcons.running) : null,
      ),
      ButtonBar(
        children: <Widget>[
          Visibility(
          visible: item.finishDate == null,
          child:FlatButton(
            child: const Text('DURDUR'),
            onPressed: () {/* ... */},
          )),
          Visibility(
          visible: item.finishDate != null,
          child:FlatButton(
            child: const Text('SİL'),
            onPressed: () {/* ... */},
          )),
        ],
      ),
    ])));
  }
}
