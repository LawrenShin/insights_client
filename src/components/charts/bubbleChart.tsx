import React from 'react';
// NOTE: no types yet provided by lib devs - https://github.com/plouc/nivo/issues/197
// @ts-ignore
import { ResponsiveBubble } from '@nivo/circle-packing';
import ChartWrapper from "../ratings/ChartWrapper";
import Legend from "./Legend";

interface DataItem {
  name: string,
  percentage: number,
}
interface Props {
  data: DataItem[],
  height?: string,
}

const breakName = (dataItem: DataItem) => {
  const splittedName: string[] = dataItem.name.split(' ');
  if (splittedName.length > 1) {
    return <>{splittedName.map(
      (word, index) => <tspan
        x={'0'}
        dy={index === 0 ? `-${splittedName.length/2.5}em` : '1em'}
      >
        {word}
      </tspan>
    )}</>;
  }
  return dataItem.name;
}

const BubbleChart = ({ height, data }: Props) => {

  return (<ChartWrapper height={height}>
    <ResponsiveBubble
      root={{
        name: 'root',
        children: [...data]
      }}
      margin={{top: 0, right: 0, bottom: 0, left: 0}}
      identity="name"
      value="percentage"
      colors={ '#A4C0FF' }
      labelTextColor={'black'}
      padding={5}
      leavesOnly={true}
      defs={[
        {
          id: 'fillColor',
          type: 'fill',
          background: '#A4C0FF',
        }
      ]}
      fill={[ { match: { depth: 1 }, id: 'fillColor' } ]}
      animate={true}
      motionStiffness={90}
      motionDamping={12}
      labelFormat={(name: string) => {
        const dataItem = data.filter((di) => di.name === name)[0];
        return dataItem.percentage > 20 ? breakName(dataItem) : '';
      }}
    />
    <Legend legend={data} />
  </ChartWrapper>);
}

export default BubbleChart;
