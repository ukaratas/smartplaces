import 'action.dart';

class Gadget {
  String name;
  String typeGroup;
  String type;
  String port;
  double value;
  String valueUnit;
  double valueToTargetRatio;
  String valueToTargetUnit;
  double valueToTarget;
  String complexValue;
  String status;
  String positionInSection;
  String attachedTo;
  List<Action> actions;
  String id;

  Gadget(
      {this.name,
      this.typeGroup,
      this.type,
      this.port,
      this.value,
      this.valueUnit,
      this.valueToTargetRatio,
      this.valueToTargetUnit,
      this.valueToTarget,
      this.complexValue,
      this.status,
      this.positionInSection,
      this.attachedTo,
      this.actions,
      this.id});

  Gadget.fromJson(Map<String, dynamic> json) {
    name = json['name'];
    typeGroup = json['type-group'];
    type = json['type'];
    port = json['port'];
    value = json['value'].toDouble();
    valueUnit = json['value-unit'];
    valueToTargetRatio = json['value-to-target-ratio'].toDouble();
    valueToTargetUnit = json['value-to-target-unit'];
    valueToTarget = json['value-to-target'].toDouble();
    complexValue = json['complex-value'];
    status = json['status'];
    positionInSection = json['position-in-section'];
    attachedTo = json['attached-to'];
    if (json['actions'] != null) {
      actions = new List<Action>();
      json['actions'].forEach((v) {
        actions.add(new Action.fromJson(v));
      });
    }
    id = json['id'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['name'] = this.name;
    data['type-group'] = this.typeGroup;
    data['type'] = this.type;
    data['port'] = this.port;
    data['value'] = this.value;
    data['value-unit'] = this.valueUnit;
    data['value-to-target-ratio'] = this.valueToTargetRatio;
    data['value-to-target-unit'] = this.valueToTargetUnit;
    data['value-to-target'] = this.valueToTarget;
    data['complex-value'] = this.complexValue;
    data['status'] = this.status;
    data['position-in-section'] = this.positionInSection;
    data['attached-to'] = this.attachedTo;
    if (this.actions != null) {
      data['actions'] = this.actions.map((v) => v.toJson()).toList();
    }
    data['id'] = this.id;
    return data;
  }
}