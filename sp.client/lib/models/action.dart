import 'package:strings/strings.dart';

class Action {
  int order;
  String canExecute;
  String targetValue;
  String targetComplexValue;
  String targetGadget;
  String id;

  Action(
      {this.order,
      this.canExecute,
      this.targetValue,
      this.targetComplexValue,
      this.targetGadget,
      this.id});

  Action.fromJson(Map<String, dynamic> json) {
    order = json['order'];
    canExecute = json['can-execute'];
    targetValue = json['target-value'];
    targetComplexValue = json['target-complex-value'];
    targetGadget = json['target-gadget'];
    id = json['id'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['"order"'] = this.order;
    data['"can-execute"'] = '"' + escape(this.canExecute) + '"';
    data['"target-value"'] = '"' +  escape(this.targetValue) + '"';
    data['"target-complex-value"'] =
        '"' + escape(this.targetComplexValue) + '"';
    data['"target-gadget"'] = '"' + this.targetGadget + '"';
    data['"id"'] = '"' + this.id + '"';
    return data;
  }
}
