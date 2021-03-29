import React from "react";
import ReactSpeedometer from "react-d3-speedometer"


const fontColorSegment = 'rgba(8, 0, 55, .4)';
const good = 'rgb(163,229,178)';
const average = 'rgb(255,234,160)';
const poor = 'rgb(255,210,210)';
const ratingMatrix = ['D', 'C', 'CC', 'CCC', 'B', 'BB', 'BBB', 'A', 'AA', 'AAA'];

let customSegmentStops: any = [];
let segmentColors: any = [];
let customSegmentLabels: any = [];

ratingMatrix.forEach((rating, i) => {
  customSegmentLabels.push({
    text: rating,
    fontSize: '13px',
    position: 'OUTSIDE',
    color: fontColorSegment,
  });
  const multiplied = (i+1) * 10;
  customSegmentStops.push(multiplied);
  segmentColors.push(
    multiplied <= 30 ? poor :
      (multiplied > 30 && multiplied <= 60) ? average : good
  );
});
//TODO: remove workaraund with err about segLabs have to be of length 9
customSegmentLabels.shift();

const RadialChart = (props: any) => {
  const {data} = props;

  return (<ReactSpeedometer
    {...props}
    minValue={10}
    maxValue={100}
    customSegmentStops={customSegmentStops}
    segmentColors={segmentColors}
    customSegmentLabels={customSegmentLabels}
    value={data.score}
  />)
}

export default RadialChart;
