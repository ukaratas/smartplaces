import 'dart:io';

import 'package:flutter/material.dart';
import 'package:smart_places/services/rest_api_client.dart';
import 'package:smart_places/widgets/settings/region_widget.dart';

class RegionListWidget extends StatefulWidget {
  RegionListWidget({Key key}) : super(key: key);

  @override
  _RegionListWidgetState createState() => _RegionListWidgetState();
}

class _RegionListWidgetState extends State<RegionListWidget> {
  var client = RestApiClient(httpClient: new HttpClient());
  @override
  Widget build(BuildContext context) {
    return ListView.builder(
        itemCount: client.settings.regions.length,
        itemBuilder: (context, index) {
          return RegionWidget(region: client.settings.regions[index]);
        });
  }
}
