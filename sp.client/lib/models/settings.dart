import 'package:equatable/equatable.dart';
import 'package:smart_places/models/region.dart';

class Settings extends Equatable {
  final List<Region> regions = List<Region>();

  Settings();

  int numberOfRegionHasGadgetsByType(String gadgetType) {
    int no = 0;
    regions.forEach((region) {
      if (region.numberOfSectionHasGadgetsByType(gadgetType) > 0) {
        no++;
      }
    });
    return no;
  }


  int numberOfRegionHasGadgetsByGroup(String gadgetGroup) {
    int no = 0;
    regions.forEach((region) {
      if (region.numberOfSectionHasGadgetsByGroup(gadgetGroup) > 0) {
        no++;
      }
    });
    return no;
  }

  Settings.fromJson(Map<String, dynamic> json) {
    if (json['regions'] != null) {
      json['regions'].forEach((v) {
        regions.add(new Region.fromJson(v));
      });
    }
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    if (this.regions != null) {
      data['regions'] = this.regions.map((v) => v.toJson()).toList();
    }
    return data;
  }

  @override
  List<Object> get props => [regions];
}
