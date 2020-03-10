

import 'package:equatable/equatable.dart';

class CleanWaterMonitoringOrder extends Equatable {
  final String id;
  final DateTime startDate;
  final DateTime finishDate;
  final double consumption;

  const CleanWaterMonitoringOrder({this.id, this.startDate, this.finishDate, this.consumption});

  @override
  List<Object> get props => [id, startDate, finishDate, consumption];

  @override
  String toString() => 'Post { id: $id }';
}