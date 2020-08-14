import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:smart_places/models/region.dart';
import 'package:smart_places/services/DecimalTextInputFormatter.dart';

class RegionModifyWidget extends StatefulWidget {
  final Region region;
  final Function(Region region) onSave;

  RegionModifyWidget({Key key, this.region, this.onSave}) : super(key: key);

  @override
  _RegionModifyWidgetState createState() => _RegionModifyWidgetState();
}

class _RegionModifyWidgetState extends State<RegionModifyWidget> {
  @override
  Widget build(BuildContext context) {
    return _form();
  }

  Widget _form() {
    //var nameController = TextEditingController(text: widget.region.name);
    //var backgroundImageController = TextEditingController(text: widget.region.backgroundImage);
    //var aspectRatioController = TextEditingController(text: widget.region.aspectRatio);

    final _formKey = GlobalKey<FormState>();

    return Form(
        key: _formKey,
        child: Column(children: <Widget>[
          Padding(
              padding: const EdgeInsets.all(10.0),
              child: new TextFormField(
                validator: (value) {
                  if (value.isEmpty) {
                    return 'Bölge adı boş olamaz';
                  }
                  return null;
                },
                initialValue: widget.region.name,
                controller: TextEditingController(),
                decoration: new InputDecoration(
                  labelText: 'Bölge Adı',
                  hintText: "Bölge adını giriniz",
                ),
              )),
          Padding(
              padding: const EdgeInsets.all(10.0),
              child: new FormField(
                builder: (FormFieldState state) {
                  return InputDecorator(
                    decoration: InputDecoration(
                      labelText: 'Bölge Tipi',
                      hintText: "Bölge tipini seçiniz",
                    ),
                    isEmpty: widget.region.type == '',
                    child: new DropdownButtonHideUnderline(
                      child: new DropdownButton(
                        value: widget.region.type,
                        isDense: true,
                        onChanged: (String newValue) {
                          setState(() {
                            widget.region.type = newValue;
                          });
                        },
                        items: <String>['Tank', 'Floor', 'Motohome', 'Boat']
                            .map((String value) {
                          return new DropdownMenuItem(
                            value: value,
                            child: new Text(value),
                          );
                        }).toList(),
                      ),
                    ),
                  );
                },
              )),
          Padding(
              padding: const EdgeInsets.all(10.0),
              child: new TextFormField(
                initialValue: widget.region.backgroundImage,
                decoration: new InputDecoration(
                  labelText: 'Arkaplan',
                  hintText: "Bölge arkaplan resmini giriniz",
                ),
              )),
          Padding(
              padding: const EdgeInsets.all(10.0),
              child: new TextFormField(
                initialValue: widget.region.aspectRatio.toString(),
                keyboardType: TextInputType.numberWithOptions(
                    decimal: true, signed: true),
                inputFormatters: <TextInputFormatter>[
                  DecimalTextInputFormatter(decimalRange: 2)
                ],
                decoration: new InputDecoration(
                  labelText: 'En/Boy Oranı',
                  hintText: "Bölgenin en/boy oranını giriniz",
                ),
              )),
          ButtonBar(
            children: <Widget>[
              FlatButton(
                  child: Text('İPTAL'),
                  onPressed: () => Navigator.pop(context)),
              FlatButton(
                  child: Text('KAYDET'),
                  color: Colors.blue,
                  onPressed: () {
                    if (_formKey.currentState.validate()) {
                      if (widget.onSave != null) {
                        //widget.region.name = nameController.text;
                        widget.onSave(widget.region);
                      }
                      Navigator.pop(context);
                    }
                  }),
            ],
          )
        ]));
  }
}
