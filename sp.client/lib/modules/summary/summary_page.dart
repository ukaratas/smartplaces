import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:smart_places/blocs/settings/settings_bloc.dart';
import 'package:smart_places/blocs/settings/settings_event.dart';
import 'package:smart_places/screens/settings/settings_full_layout_widget.dart';
import 'package:smart_places/widget/drawer.dart';

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
        drawer: AppDrawer(),
        appBar: new AppBar(
          title: new Text('Özet Bilgiler'),
        ),
        body: BlocProvider(
            create: (context) => SettingsBloc()
               ..add(GetSettings()),
            child: SettingsFullLayoutWidget()));
  }
}
