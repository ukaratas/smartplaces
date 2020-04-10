import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:smart_places/blocs/settings/settings_bloc.dart';
import 'package:smart_places/blocs/settings/settings_state.dart';
import 'package:smart_places/widgets/settings/region_widget.dart';

class RegionListWidget extends StatefulWidget {
  RegionListWidget({Key key}) : super(key: key);

  @override
  _RegionListWidgetState createState() => _RegionListWidgetState();
}

class _RegionListWidgetState extends State<RegionListWidget> {
  @override
  Widget build(BuildContext context) {
    return BlocBuilder<SettingsBloc, SettingsState>(builder: (context, state) {
      if (state is SettingsNotLoaded) {
        return Center(
          child: CircularProgressIndicator(),
        );
      }

      if (state is SettingsError) {
        return Center(
          child: Text('state == failed to fetch settings'),
        );
      }

      if (state is SettingsLoaded) {
        return ListView.builder(
            itemCount: state.settings.regions.length,
            itemBuilder: (context, index) {
              return RegionWidget(region: state.settings.regions[index]);
            });
      }

      return Center(
        child: Text('@ Not implemented state'),
      );
    });
  }
}
