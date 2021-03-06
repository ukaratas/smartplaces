import 'package:smart_places/models/row.dart';
import 'package:smart_places/models/section.dart';

class Region {
  String name;
  String type;
  String backgroundImage;
  double aspectRatio;
  List<Section> sections;
  List<Row> rows;
  int layoutRowsCount;
  int layoutColumnCount;
  String id;

  Region(
      {this.name,
      this.type,
      this.backgroundImage,
      this.aspectRatio,
      this.sections,
      this.rows,
      this.id});

  bool get hasVirtualLayoutInfo {
    return aspectRatio > 0 &&
        backgroundImage.isNotEmpty &&
        layoutColumnCount > 0 &&
        layoutRowsCount > 0;
  }

  int numberOfSectionHasGadgetsByType(String gadgetType) {
    int no = 0;
    sections.forEach((section) {
      if (section.numberOfGadgetsByType(gadgetType) > 0) {
        no++;
      }
    });
    return no;
  }

  int numberOfSectionHasGadgetsByGroup(String gadgetGroup) {
    int no = 0;
    sections.forEach((section) {
      if (section.numberOfGadgetsByGroup(gadgetGroup) > 0) {
        no++;
      }
    });
    return no;
  }

  Region copy() {
    return Region(
        name: this.name,
        type: this.type,
        backgroundImage: this.backgroundImage,
        aspectRatio: this.aspectRatio,
        id: this.id);
  }

  update(Region _region) {
    this.name = _region.name;
    this.type = _region.type;
    this.backgroundImage = _region.backgroundImage;
    this.aspectRatio = _region.aspectRatio;
  }

  Region.fromJson(Map<String, dynamic> json) {
    name = json['name'];
    type = json['type'];
    backgroundImage = json['background-image'];
    aspectRatio = json['aspect-ratio'].toDouble();
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
    layoutRowsCount = json['layout-rows-count'];
    layoutColumnCount = json['layout-column-count'];
    id = json['id'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['"name"'] = '"' + this.name + '"';
    data['"type"'] = '"' +  this.type + '"';
    data['"background-image"'] = '"' + this.backgroundImage + '"';
    data['"aspect-ratio"'] = this.aspectRatio;

    if (this.sections != null) {
      data['"sections"'] = this.sections.map((v) => v.toJson()).toList();
    }
    if (this.rows != null) {
      data['"rows"'] = this.rows.map((v) => v.toJson()).toList();
    }
    data['"id"'] = '"' + this.id + '"';
    return data;
  }
}
