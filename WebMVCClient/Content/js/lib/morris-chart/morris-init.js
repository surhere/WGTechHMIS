// Dashboard 1 Morris-chart
$( function () {
	"use strict";

		// Morris donut chart

	Morris.Donut( {
		element: 'morris-donut-chart',
		data: [ {
			label: "Mediclaim Others",
			value: 45,

        }, {
			label: "RSVY",
			value: 20
        }, {
			label: "Swasthya Sathi",
			value: 25
        }, {
			label: "General",
			value: 10
        } ],
		resize: true,
		colors: [ '#4680ff', '#26DAD2', '#2ccc22', '#fc6180' ]
	} );

	
	// Morris bar chart
	Morris.Bar( {
		element: 'morris-bar-chart',
		data: [ {
			y: 'Today',
			a: 100,
			b: 60,
			c: 40
        }, {
			y: 'Yesterday',
			a: 75,
			b: 35,
			c: 40
        }, {
			y: '29-3-2018',
			a: 50,
			b: 35,
			c: 15
        }, {
			y: '28-3-2018',
			a: 75,
			b: 25,
			c: 50
        }, {
			y: '27-3-2018',
			a: 50,
			b: 30,
			c: 20
        }, {
			y: '26-3-2018',
			a: 75,
			b: 55,
			c: 20
        }],
		xkey: 'y',
		ykeys: [ 'a', 'b', 'c' ],
		labels: [ 'Total Patient', 'Packaged', 'Normal' ],
		barColors: [ '#26DAD2', '#fc6180', '#4680ff' ],
		hideHover: 'auto',
		gridLineColor: '#eef0f2',
		resize: true
	} );

	// LINE CHART
	var line = new Morris.Line( {
		element: 'morris-line-chart',
		resize: true,
		data: [
			{
				y: '2011 Q1',
				item1: 2666
			},
			{
				y: '2011 Q2',
				item1: 2778
			},
			{
				y: '2011 Q3',
				item1: 4912
			},
			{
				y: '2011 Q4',
				item1: 3767
			},
			{
				y: '2012 Q1',
				item1: 6810
			},
			{
				y: '2012 Q2',
				item1: 5670
			},
			{
				y: '2012 Q3',
				item1: 4820
			},
			{
				y: '2012 Q4',
				item1: 15073
			},
			{
				y: '2013 Q1',
				item1: 10687
			},
			{
				y: '2013 Q2',
				item1: 8432
			}
          ],
		xkey: 'y',
		ykeys: [ 'item1' ],
		labels: [ 'Item 1' ],
		gridLineColor: '#eef0f2',
		lineColors: [ '#4680ff' ],
		lineWidth: 1,
		hideHover: 'auto'
	} );



	// Extra chart
	Morris.Area( {
		element: 'extra-area-chart',
		data: [ {
				period: '2001',
				smartphone: 0,
				windows: 0,
				mac: 0
        }, {
				period: '2002',
				smartphone: 90,
				windows: 60,
				mac: 25
        }, {
				period: '2003',
				smartphone: 40,
				windows: 80,
				mac: 35
        }, {
				period: '2004',
				smartphone: 30,
				windows: 47,
				mac: 17
        }, {
				period: '2005',
				smartphone: 150,
				windows: 40,
				mac: 120
        }, {
				period: '2006',
				smartphone: 25,
				windows: 80,
				mac: 40
        }, {
				period: '2007',
				smartphone: 10,
				windows: 10,
				mac: 10
        }


        ],
		lineColors: [ '#26DAD2', '#fc6180', '#4680ff' ],
		xkey: 'period',
		ykeys: [ 'smartphone', 'windows', 'mac' ],
		labels: [ 'Phone', 'Windows', 'Mac' ],
		pointSize: 0,
		lineWidth: 0,
		resize: true,
		fillOpacity: 0.8,
		behaveLikeLine: true,
		gridLineColor: '#e0e0e0',
		hideHover: 'auto'

	} );
	Morris.Area( {
		element: 'morris-area-chart',
		data: [ {
				period: '2001',
				smartphone: 0,
				windows: 0,
				mac: 0
        }, {
				period: '2002',
				smartphone: 90,
				windows: 60,
				mac: 25
        }, {
				period: '2003',
				smartphone: 40,
				windows: 80,
				mac: 35
        }, {
				period: '2004',
				smartphone: 30,
				windows: 47,
				mac: 17
        }, {
				period: '2005',
				smartphone: 150,
				windows: 40,
				mac: 120
        }, {
				period: '2006',
				smartphone: 25,
				windows: 80,
				mac: 40
        }, {
				period: '2007',
				smartphone: 10,
				windows: 10,
				mac: 10
        }


        ],
		xkey: 'period',
		ykeys: [ 'smartphone', 'windows', 'mac' ],
		labels: [ 'Phone', 'Windows', 'Mac' ],
		pointSize: 3,
		fillOpacity: 0,
		pointStrokeColors: [ '#26DAD2', '#4680ff', '#fc6180' ],
		behaveLikeLine: true,
		gridLineColor: '#e0e0e0',
		lineWidth: 3,
		hideHover: 'auto',
		lineColors: [ '#26DAD2', '#4680ff', '#fc6180' ],
		resize: true

	} );

	Morris.Area( {
		element: 'morris-area-chart2',
		data: [ {
				period: '2010',
				SiteA: 0,
				SiteB: 0,

        }, {
				period: '2011',
				SiteA: 130,
				SiteB: 100,

        }, {
				period: '2012',
				SiteA: 80,
				SiteB: 60,

        }, {
				period: '2013',
				SiteA: 70,
				SiteB: 200,

        }, {
				period: '2014',
				SiteA: 180,
				SiteB: 150,

        }, {
				period: '2015',
				SiteA: 105,
				SiteB: 90,

        },
			{
				period: '2016',
				SiteA: 250,
				SiteB: 150,

        } ],
		xkey: 'period',
		ykeys: [ 'SiteA', 'SiteB' ],
		labels: [ 'Site A', 'Site B' ],
		pointSize: 0,
		fillOpacity: 0.4,
		pointStrokeColors: [ '#b4becb', '#4680ff' ],
		behaveLikeLine: true,
		gridLineColor: '#e0e0e0',
		lineWidth: 0,
		smooth: false,
		hideHover: 'auto',
		lineColors: [ '#b4becb', '#4680ff' ],
		resize: true

	} );


} );
