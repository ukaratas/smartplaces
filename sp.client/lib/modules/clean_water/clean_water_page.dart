import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:smart_places/widget/drawer.dart';

import 'clean_water_monitoring_order_list_bloc.dart';
import 'clean_water_monitoring_order_list_event.dart';
import 'clean_water_monitoring_order_list_widget.dart';

class CleanWaterPage extends StatefulWidget {
  CleanWaterPage({Key key}) : super(key: key);

  static const String routeName = '/clean-water';

  @override
  _CleanWaterPageState createState() => _CleanWaterPageState();
}

class _CleanWaterPageState extends State<CleanWaterPage> {
  @override
  Widget build(BuildContext context) {
    return new Scaffold(
      drawer: AppDrawer(),
      appBar: new AppBar(
        title: new Text('Temiz Su'),
      ),
      body: BlocProvider(
        create: (context) =>
            CleanWaterMonitoringOrderListBloc(httpClient: http.Client())
              ..add(Fetch()),
        child: CleanWaterMonitoringOrderListWidget(),
      ),
    );
  }
}

/*
class _CleanWaterPageState extends State<CleanWaterPage> {
  @override
  Widget build(BuildContext context) {
    return new Scaffold(
        drawer: AppDrawer(),
        appBar: new AppBar(
          title: new Text('Temiz Su'),
        ),
        body:
         ListView(
          //mainAxisAlignment: MainAxisAlignment.start,
          //crossAxisAlignment: CrossAxisAlignment.stretch,
          children: [
            new CircularPercentIndicator(
              radius: 180.0,
              lineWidth: 25.0,
              animation: true,
              percent: 0.70,
              center: Column(
                mainAxisAlignment: MainAxisAlignment.center,
                children: [
                  new Text("Depo Durumu",
                      style: new TextStyle(
                          fontWeight: FontWeight.bold, fontSize: 15.0)),
                  new Text("%70",
                      style: new TextStyle(
                          fontWeight: FontWeight.bold, fontSize: 20.0)),
                  new Text("126 Litre",
                      style: new TextStyle(
                          fontWeight: FontWeight.bold, fontSize: 12.0))
                ],
              ),
              circularStrokeCap: CircularStrokeCap.round,
              progressColor: Colors.green,
            ),
            new CircularPercentIndicator(
              radius: 180.0,
              lineWidth: 25.0,
              animation: true,
              percent: 0.40,
              center: Column(
                mainAxisAlignment: MainAxisAlignment.center,
                children: [
                  new Text("Anlık Tüketim",
                      style: new TextStyle(
                          fontWeight: FontWeight.bold, fontSize: 15.0)),
                  new Text("5",
                      style: new TextStyle(
                          fontWeight: FontWeight.bold, fontSize: 20.0)),
                  new Text("Litre/Dakika",
                      style: new TextStyle(
                          fontWeight: FontWeight.bold, fontSize: 12.0))
                ],
              ),
              circularStrokeCap: CircularStrokeCap.round,
              progressColor: Colors.deepOrange,
            ),
            _buildMonitorCard(context),
            _buildMonitorCard(context),
            _buildMonitorCard(context),
            Expanded(
                child: SizedBox(
                    height: 200.0,
                    child: BlocProvider(
                      create: (context) => CleanWaterMonitoringOrderListBloc(
                          httpClient: http.Client())
                        ..add(Fetch()),
                      child: CleanWaterMonitoringOrderListWidget(),
                    )))
          ],
        ));
  }
}

Widget _buildMonitorCard(BuildContext context) {
  return Center(
    child: Card(
      child: Column(
        mainAxisSize: MainAxisSize.min,
        children: <Widget>[
          const ListTile(
            leading: Icon(FontAwesomeIcons.hourglassStart),
            title: Text('01 Ocak 2020 - 10:52:18'),
            subtitle: Text('Başlangıç Zamanı'),
          ),
          const ListTile(
            leading: Icon(FontAwesomeIcons.hourglassEnd),
            title: Text('01 Ocak 2020 - 11:42:18'),
            subtitle: Text('Tamamlanma Zamanı'),
          ),
          const ListTile(
            leading: Icon(FontAwesomeIcons.toiletPaper),
            title: Text('18.5 Litre'),
            subtitle: Text('Tüketim'),
            trailing: Icon(FontAwesomeIcons.running),
          ),
          ButtonBar(
            children: <Widget>[
              FlatButton(
                child: const Text('DURDUR'),
                onPressed: () {/* ... */},
              ),
              FlatButton(
                child: const Text('SİL'),
                onPressed: () {/* ... */},
              ),
            ],
          ),
        ],
      ),
    ),
  );
}

enum MonitoringStatusType {
  active,
  completed,
}

class CleanWaterTankMonitoringItem {
  CleanWaterTankMonitoringItem({
    @required this.startDate,
    @required this.consumption,
    @required this.status,
    this.finishDate,
  });

  final DateTime startDate;
  final DateTime finishDate;
  final double consumption;
  final MonitoringStatusType status;
}

List<CleanWaterTankMonitoringItem> monitorings = <CleanWaterTankMonitoringItem>[
  new CleanWaterTankMonitoringItem(
      startDate: new DateTime.utc(2020, 1, 1, 10, 11),
      consumption: 0,
      status: MonitoringStatusType.active),
  new CleanWaterTankMonitoringItem(
    startDate: new DateTime.utc(2020, 1, 1, 10, 11),
    consumption: 0,
    status: MonitoringStatusType.completed,
    finishDate: new DateTime.utc(2020, 1, 1, 11, 10),
  ),
];
*/
