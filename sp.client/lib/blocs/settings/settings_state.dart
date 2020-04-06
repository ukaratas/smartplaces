import 'package:equatable/equatable.dart';
import 'package:smart_places/models/settings.dart';

abstract class SettingsState extends Equatable {
  const SettingsState();

  @override
  List<Object> get props => [];
}

class SettingsNotLoaded extends SettingsState {
  const SettingsNotLoaded() : super();

  @override
  String toString() => 'Loading';
}

class SettingsLoaded extends SettingsState {
  final Settings settings;

  const SettingsLoaded({this.settings}) : super();

  @override
  String toString() => 'Ready';
}

class SettingsError extends SettingsState {
  final String error;

  const SettingsError({this.error}) : super();

  @override
  String toString() => 'Error';
}
