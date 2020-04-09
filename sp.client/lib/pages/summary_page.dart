import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:smart_places/blocs/settings/settings_bloc.dart';
import 'package:smart_places/blocs/settings/settings_event.dart';
import 'package:smart_places/widgets/drawer/drawer_widget.dart';

class SummaryPage extends StatefulWidget {
  SummaryPage({Key key}) : super(key: key);

  static const String routeName = '/summary';

  @override
  _SummaryPageState createState() => _SummaryPageState();
}

class _SummaryPageState extends State<SummaryPage> {
  @override
  Widget build(BuildContext context) {
    return new Scaffold(
        backgroundColor: Colors.white,
        drawer: BlocProvider(
            create: (context) => SettingsBloc()..add(GetSettings()),
            child: DrawerWidget()),
        appBar: new AppBar(
          title: new Text('Özet Bilgiler'),
        ),
        body: Center(child: Text('Özet Anasayfa')));
  }
}
