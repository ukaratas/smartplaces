import 'package:flutter/material.dart';

class PlaceLayout extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Table(
      border: TableBorder.all(),
      defaultVerticalAlignment: TableCellVerticalAlignment.top,
      children: <TableRow>[
        TableRow(children: <Widget>[
          FittedBox(
            fit: BoxFit.contain,
            child: Container(
              margin: EdgeInsets.all(2),
              color: Colors.red,
              child: Center(
                child: Text(
                  "Row 1 Element 1",
                ),
              ),
            ),
          ),
        ]),
      ],
    );
  }
}
