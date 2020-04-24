import 'package:smart_places/models/gadget.dart';

class Section {
  String name;
  List<Gadget> gadgets;
  int row;
  int column;
  String id;

  Section({this.name, this.gadgets, this.row, this.column, this.id});

  int numberOfGadgetsByType(String gadgetType) {
    int no = 0;
    gadgets.forEach((gadget) {
      if (gadget.type == gadgetType) {
        no++;
      }
    });
    return no;
  }

  int numberOfGadgetsByGroup(String gadgetGroup) {
    int no = 0;
    gadgets.forEach((gadget) {
      if (gadget.typeGroup == gadgetGroup) {
        no++;
      }
    });
    return no;
  }

  Section.fromJson(Map<String, dynamic> json) {
    name = json['name'];
    if (json['gadgets'] != null) {
      gadgets = new List<Gadget>();
      json['gadgets'].forEach((v) {
        gadgets.add(new Gadget.fromJson(v));
      });
    }
    row = json['row'];
    column = json['column'];
    id = json['id'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['"name"'] = '"' + this.name + '"';
    if (this.gadgets != null) {
      data['"gadgets"'] = this.gadgets.map((v) => v.toJson()).toList();
    }
    data['"row"'] = this.row;
    data['"column"'] = this.column;
    data['"id"'] =  '"' +  this.id + '"';
    return data;
  }
}
