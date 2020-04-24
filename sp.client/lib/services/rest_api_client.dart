import 'dart:io';
import 'package:http/io_client.dart';
import 'dart:convert';
import 'package:meta/meta.dart';
import 'package:smart_places/models/region.dart';
import 'package:smart_places/models/settings.dart';

class RestApiClient {
  static const baseUrl = 'https://10.0.2.2:5001';

  static Settings settings;

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
    settings = Settings.fromJson(data);

    return settings;
  }

  Future updateRegion(Region region) async {}

  Future updateSettings() async {
    final settingsUrl = '$baseUrl/settings';
    IOClient ioClient = new IOClient(httpClient);
    final String updateValue = settings.toJson().toString();

    final response = await ioClient.post(settingsUrl,
        headers: {
          "content-type": "application/json",
          "accept": "application/json",
        },
        body: updateValue);

    print(response.body);

    if (response.statusCode != 200) {
      throw Exception('error updating settings');
    }
  }
}
