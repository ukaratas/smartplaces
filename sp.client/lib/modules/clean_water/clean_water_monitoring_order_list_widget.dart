import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'clean_water_consumption_widget.dart';
import 'clean_water_monitoring_order.dart';
import 'clean_water_monitoring_order_list_bloc.dart';
import 'clean_water_monitoring_order_list_event.dart';
import 'clean_water_monitoring_order_list_state.dart';
import 'clean_water_monitoring_order_widget.dart';
import 'clean_water_status_widget.dart';

class CleanWaterMonitoringOrderListWidget extends StatefulWidget {
  @override
  _CleanWaterMonitoringOrderListWidgetState createState() =>
      _CleanWaterMonitoringOrderListWidgetState();
}

class _CleanWaterMonitoringOrderListWidgetState
    extends State<CleanWaterMonitoringOrderListWidget> {
  final _scrollController = ScrollController();
  final _scrollThreshold = 200.0;
  CleanWaterMonitoringOrderListBloc _cleanWaterMonitoringOrderListBloc;

  @override
  void initState() {
    super.initState();
    _scrollController.addListener(_onScroll);
    _cleanWaterMonitoringOrderListBloc =
        BlocProvider.of<CleanWaterMonitoringOrderListBloc>(context);
  }

  @override
  Widget build(BuildContext context) {
    return BlocBuilder<CleanWaterMonitoringOrderListBloc,
        CleanWaterMonitoringOrderListState>(
      builder: (context, state) {
        if (state is CleanWaterMonitoringOrderListUninitialized) {
          return Center(
            child: CircularProgressIndicator(),
          );
        }
        if (state is CleanWaterMonitoringOrderListError) {
          return Center(
            child: Text('state == failed to fetch posts'),
          );
        }
        if (state is CleanWaterMonitoringOrderListLoaded) {
          if (state.items.isEmpty) {
            return Center(
              child: Text('no posts'),
            );
          }
          return ListView.builder(
            itemBuilder: (BuildContext context, int index) {
              if (index == 0) {
                return Column(children: [
                  CleanWaterStatusWidget(),
                  CleanWaterConsumptionWidget(),
                ]);
              } else {
                return index >= state.items.length
                    ? BottomLoader()
                    : CleanWaterMinitoringOrderWidget(item: state.items[index]);
              }
            },
            itemCount: state.hasReachedMax
                ? state.items.length
                : state.items.length + 1,
            controller: _scrollController,
          );
        }
      },
    );
  }

  @override
  void dispose() {
    _scrollController.dispose();
    super.dispose();
  }

  void _onScroll() {
    final maxScroll = _scrollController.position.maxScrollExtent;
    final currentScroll = _scrollController.position.pixels;
    if (maxScroll - currentScroll <= _scrollThreshold) {
      _cleanWaterMonitoringOrderListBloc.add(Fetch());
    }
  }
}

class BottomLoader extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Container(
      alignment: Alignment.center,
      child: Center(
        child: SizedBox(
          width: 33,
          height: 33,
          child: CircularProgressIndicator(
            strokeWidth: 1.5,
          ),
        ),
      ),
    );
  }
}
/*
class PostWidget extends StatelessWidget {
  final CleanWaterMonitoringOrder post;

  const PostWidget({Key key, @required this.post}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return ListTile(
      leading: Text(
        '${post.id}',
        style: TextStyle(fontSize: 10.0),
      ),
      title: Text(post.startDate),
      isThreeLine: true,
      subtitle: Text(post.consumption.toString()),
      dense: true,
    );
  }
}
*/