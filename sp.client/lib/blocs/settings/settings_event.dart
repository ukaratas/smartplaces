import 'package:equatable/equatable.dart';
import 'package:smart_places/models/region.dart';

abstract class SettingsEvent extends Equatable {
  @override
  List<Object> get props => [];
}

class GetSettings extends SettingsEvent {}

class UpdateRegion extends SettingsEvent {
  
  final Region region;

  UpdateRegion(this.region);
}
