import 'package:smart_places/models/row.dart';
import 'package:smart_places/models/section.dart';

class Region {
  String name;
  String type;
  List<Section> sections;
  List<Row> rows;
  String id;

  Region({this.name, this.type, this.sections, this.rows, this.id});

  Region.fromJson(Map<String, dynamic> json) {
    name = json['name'];
    type = json['type'];
    if (json['sections'] != null) {
      sections = new List<Section>();
      json['sections'].forEach((v) {
        sections.add(new Section.fromJson(v));
      });
    }
    if (json['rows'] != null) {
      rows = new List<Row>();
      json['rows'].forEach((v) {
        rows.add(new Row.fromJson(v));
      });
    }
    id = json['id'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['name'] = this.name;
    data['type'] = this.type;
    if (this.sections != null) {
      data['sections'] = this.sections.map((v) => v.toJson()).toList();
    }
    if (this.rows != null) {
      data['rows'] = this.rows.map((v) => v.toJson()).toList();
    }
    data['id'] = this.id;
    return data;
  }
}
