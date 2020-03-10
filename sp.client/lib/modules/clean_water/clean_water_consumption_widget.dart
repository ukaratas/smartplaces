import 'package:flutter/material.dart';
import 'package:percent_indicator/circular_percent_indicator.dart';

class CleanWaterConsumptionWidget extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Center(
        child: new CircularPercentIndicator(
      radius: 180.0,
      lineWidth: 25.0,
      animation: true,
      percent: 0.40,
      center: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        children: [
          new Text("Anlık Tüketim",
              style:
                  new TextStyle(fontWeight: FontWeight.bold, fontSize: 15.0)),
          new Text("5",
              style:
                  new TextStyle(fontWeight: FontWeight.bold, fontSize: 20.0)),
          new Text("Litre/Dakika",
              style: new TextStyle(fontWeight: FontWeight.bold, fontSize: 12.0))
        ],
      ),
      circularStrokeCap: CircularStrokeCap.round,
      progressColor: Colors.deepOrange,
    ));
  }
}
