import 'package:flutter/material.dart';
import 'package:percent_indicator/circular_percent_indicator.dart';

class CleanWaterStatusWidget extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Center(
      child: new CircularPercentIndicator(
        radius: 180.0,
        lineWidth: 25.0,
        animation: true,
        percent: 0.70,
        center: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            new Text("Depo Durumu",
                style:
                    new TextStyle(fontWeight: FontWeight.bold, fontSize: 15.0)),
            new Text("%70",
                style:
                    new TextStyle(fontWeight: FontWeight.bold, fontSize: 20.0)),
            new Text("126 Litre",
                style:
                    new TextStyle(fontWeight: FontWeight.bold, fontSize: 12.0))
          ],
        ),
        circularStrokeCap: CircularStrokeCap.round,
        progressColor: Colors.green,
      ),
    );
  }
}
