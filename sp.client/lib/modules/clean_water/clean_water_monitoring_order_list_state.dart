import 'package:equatable/equatable.dart';

import 'clean_water_monitoring_order.dart';

abstract class CleanWaterMonitoringOrderListState extends Equatable {
  const CleanWaterMonitoringOrderListState();

  @override
  List<Object> get props => [];
}

class CleanWaterMonitoringOrderListUninitialized
    extends CleanWaterMonitoringOrderListState {}

class CleanWaterMonitoringOrderListError
    extends CleanWaterMonitoringOrderListState {}

class CleanWaterMonitoringOrderListLoaded
    extends CleanWaterMonitoringOrderListState { 
  final List<CleanWaterMonitoringOrder> items;
  final bool hasReachedMax;

  const CleanWaterMonitoringOrderListLoaded({
    this.items,
    this.hasReachedMax,
  });

  CleanWaterMonitoringOrderListLoaded copyWith({
    List<CleanWaterMonitoringOrder> items,
    bool hasReachedMax,
  }) {
    return CleanWaterMonitoringOrderListLoaded(
      items: items ?? this.items,
      hasReachedMax: hasReachedMax ?? this.hasReachedMax,
    );
  }

  @override
  List<Object> get props => [items, hasReachedMax];

  @override
  String toString() =>
      'PostLoaded { posts: ${items.length}, hasReachedMax: $hasReachedMax }';
}
