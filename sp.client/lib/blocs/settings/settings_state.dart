import 'package:equatable/equatable.dart';
import 'package:smart_places/models/settings.dart';

abstract class SettingState extends Equatable {
  final Settings settings = null;
  final String error = null;

  const SettingState({Settings settings, String error});

  @override
  List<Object> get props => [settings];
}

class SettingsNotLoaded extends SettingState {
  const SettingsNotLoaded() : super();

  @override
  String toString() => 'Loading';
}

class SettingsLoaded extends SettingState {
  const SettingsLoaded(Settings _settings) : super(settings: _settings);

  @override
  String toString() => 'Ready';
}

class SettingsError extends SettingState {
  const SettingsError(String _error) : super(error: _error);

  @override
  String toString() => 'Error';
}
