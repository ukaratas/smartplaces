import 'package:flutter/material.dart';
import 'package:smart_places/modules/clean_water/clean_water_page.dart';

import 'modules/lighting/lighting_page.dart';
import 'modules/summary/summary_page.dart';

import 'routes.dart';

void main() => {
  
  
  runApp(MyApp())
  };

class MyApp extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      color: Colors.orangeAccent,
      initialRoute: Routes.summary,
      routes: {
        Routes.summary: (context) => SummaryPage(),
        Routes.lighting: (context) => LightingPage(),
        Routes.cleanWater: (context) => CleanWaterPage(),
      },
    );
  }
}
