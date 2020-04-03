import 'dart:io';
import 'package:bloc/bloc.dart';
import 'package:smart_places/blocs/settings/settings_event.dart';
import 'package:smart_places/blocs/settings/settings_state.dart';
import 'package:smart_places/models/settings.dart';
import 'package:smart_places/services/rest_api_client.dart';

class SettingsBloc extends Bloc<SettingsEvent, SettingState> {
  SettingsBloc();

  @override
  SettingState get initialState => SettingsNotLoaded();

  @override
  Stream<SettingState> mapEventToState(SettingsEvent event) async* {
    final currentState = state;
    if (event is GetSettings) {
      try {
        if (currentState is SettingsNotLoaded) {
          final settings = await _fetchSettings();
          yield SettingsLoaded(settings);
          return;
        }
      } catch (e) {
        yield SettingsError(e.toString());
      }
    }
  }

  Future<Settings> _fetchSettings() async {
    final settingsClient = new RestApiClient(httpClient: new HttpClient());
    return settingsClient.getSettings();
  }
}
