import 'package:equatable/equatable.dart';
import 'package:smart_places/models/region.dart';

class Settings extends Equatable {
  List<Region> regions;

  Settings({this.regions});

  Settings.fromJson(Map<String, dynamic> json) {
    if (json['regions'] != null) {
      regions = new List<Region>();
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
  
  List<Object> get props =>[
        regions
      ];

  
}
