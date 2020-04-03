
class Row {
  int no;
  int percetage;
  String id;

  Row({this.no, this.percetage, this.id});

  Row.fromJson(Map<String, dynamic> json) {
    no = json['no'];
    percetage = json['percetage'];
    id = json['id'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['no'] = this.no;
    data['percetage'] = this.percetage;
    data['id'] = this.id;
    return data;
  }
}
