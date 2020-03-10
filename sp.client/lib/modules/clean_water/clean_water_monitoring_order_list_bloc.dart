import 'dart:convert';

import 'package:bloc/bloc.dart';
import 'package:meta/meta.dart';
import 'package:http/http.dart' as http;

import 'clean_water_monitoring_order.dart';
import 'clean_water_monitoring_order_list_event.dart';
import 'clean_water_monitoring_order_list_state.dart';

class CleanWaterMonitoringOrderListBloc extends Bloc<
    CleanWaterMonitoringOrderListEvent, CleanWaterMonitoringOrderListState> {
  final http.Client httpClient;

  CleanWaterMonitoringOrderListBloc({@required this.httpClient});

/*
  @override
  Stream<CleanWaterMonitoringOrderListState> transformEvents(
    Stream<CleanWaterMonitoringOrderListEvent> events,
    Stream<CleanWaterMonitoringOrderListState> Function(CleanWaterMonitoringOrderListEvent event) next,
  ) {
    return super.transformEvents(
      events.debounceTime(
        Duration(milliseconds: 500),
      ),
      next,
    );
  }
*/
  @override
  CleanWaterMonitoringOrderListState get initialState =>
      CleanWaterMonitoringOrderListUninitialized();

  @override
  Stream<CleanWaterMonitoringOrderListState> mapEventToState(
      CleanWaterMonitoringOrderListEvent event) async* {
    final currentState = state;
    if (event is Fetch && !_hasReachedMax(currentState)) {
      try {
        if (currentState is CleanWaterMonitoringOrderListUninitialized) {
          final posts = await _fetchPosts(0, 3);
          yield CleanWaterMonitoringOrderListLoaded(
              items: posts, hasReachedMax: false);
          return;
        }
        if (currentState is CleanWaterMonitoringOrderListLoaded) {
          final posts = await _fetchPosts(currentState.items.length, 2);
          yield posts.isEmpty
              ? currentState.copyWith(hasReachedMax: true)
              : CleanWaterMonitoringOrderListLoaded(
                  items: currentState.items + posts,
                  hasReachedMax: false,
                );
        }
      } catch (_) {
        yield CleanWaterMonitoringOrderListError();
      }
    }
  }

  bool _hasReachedMax(CleanWaterMonitoringOrderListState state) =>
      state is CleanWaterMonitoringOrderListLoaded && state.hasReachedMax;

  Future<List<CleanWaterMonitoringOrder>> _fetchPosts(
      int startIndex, int limit) async {
    final response = await httpClient.get(
        'https://jsonbox.io/box_15a9873be8a6e7dad609?skip=$startIndex&limit=$limit');
    if (response.statusCode == 200) {
      final data = json.decode(response.body) as List;
      return data.map((rawPost) {
        return CleanWaterMonitoringOrder(
          id: rawPost['_id'],
          startDate: DateTime.parse(rawPost['start-date']),
          finishDate: rawPost['finish-date'] != null
              ? DateTime.parse(rawPost['finish-date'])
              : null,
          consumption: rawPost['consumption'],
        );
      }).toList();
    } else {
      throw Exception('error fetching posts');
    }
  }
}
