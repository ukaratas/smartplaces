import 'dart:io';
import 'package:bloc/bloc.dart';
import 'package:smart_places/blocs/settings/settings_event.dart';
import 'package:smart_places/blocs/settings/settings_state.dart';
import 'package:smart_places/models/settings.dart';
import 'package:smart_places/services/rest_api_client.dart';

class SettingsBloc extends Bloc<SettingsEvent, SettingsState> {
  SettingsBloc();

  @override
  SettingsState get initialState => SettingsNotLoaded();

  @override
  Stream<SettingsState> mapEventToState(SettingsEvent event) async* {
    final currentState = state;
    if (event is GetSettings) {
      try {
        if (currentState is SettingsNotLoaded) {
          final settings = await _fetchSettings();
          yield SettingsLoaded(settings: settings);
          return;
        }
      } catch (e) {
        yield SettingsError(error: e.toString());
      }
    }

    if (event is SaveRegion) {
      try {
        var settingsClient = new RestApiClient(httpClient: new HttpClient());
        await settingsClient.saveRegion(event.region);
        yield SettingsLoaded(settings: settingsClient.settings);
        return;
      } catch (e) {
        yield SettingsError(error: e.toString());
      }
    }
  }

  Future<Settings> _fetchSettings() async {
    final settingsClient = new RestApiClient(httpClient: new HttpClient());
    return settingsClient.getSettings();
  }
}
