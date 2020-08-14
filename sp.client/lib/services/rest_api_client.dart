import 'dart:io';
import 'package:http/io_client.dart';
import 'dart:convert';
import 'package:meta/meta.dart';
import 'package:smart_places/blocs/settings/settings_event.dart';
import 'package:smart_places/models/region.dart';
import 'package:smart_places/models/settings.dart';

class RestApiClient {
  static const baseUrl = 'https://10.0.2.2:5001';

  static Settings _settings;

  Settings get settings {
    if (_settings == null)
    {
      GetSettings();
    }
    return _settings;
  }

  final HttpClient httpClient;

  RestApiClient({
    @required this.httpClient,
  }) : assert(httpClient != null) {
    this.httpClient.badCertificateCallback =
        (X509Certificate cert, String host, int port) => true;
  }

  Future<Settings> getSettings() async {
    final settingsUrl = '$baseUrl/settings';
    IOClient ioClient = new IOClient(httpClient);
    final response = await ioClient.get(settingsUrl);

    if (response.statusCode != 200) {
      throw Exception('error getting settings');
    }
    final data = json.decode(response.body);
    _settings = Settings.fromJson(data);

    return _settings;
  }

  Future saveRegion(Region region) async {
    if (_settings.saveRegion(region)) {
      updateSettings();
    }
  }

  Future updateSettings() async {
    final settingsUrl = '$baseUrl/settings';
    IOClient ioClient = new IOClient(httpClient);
    final String updateValue = _settings.toJson().toString();

    final response = await ioClient.post(settingsUrl,
        headers: {
          "content-type": "application/json",
          "accept": "application/json",
        },
        body: updateValue);

    if (response.statusCode == 200) {
      var returnObject = jsonDecode(response.body);
      _settings = Settings.fromJson(returnObject["item"]);
    } else {
      print(response);
      throw Exception('error updating settings');
    }
  }
}
