import React from 'react';
import { ResponsiveRadar } from '@nivo/radar'
import {keyTitle} from "../../helpers";


const radarDataPreparer = (data: {[key: string]: any}) => {
  return Object.keys(data).map((key) => {
    return {
      key: keyTitle(key.replace(/advanced|essential/gi, '')),
      rating: data[key].score,
    }
  });
}

const Radar = (props: any) => {
  const redata = radarDataPreparer(props.data)
  if (!redata) return <>No data</>;

  return (
    <div style={{position: 'relative', height: '400px', width: '100%'}}>
      <div style={{position: 'absolute', height: '400px', width: '100%'}}>
        <ResponsiveRadar
          data={redata}
          keys={[ 'rating' ]}
          indexBy="key"
          maxValue="auto"
          margin={{ top: 70, right: 80, bottom: 40, left: 80 }}
          curve="linearClosed"
          borderWidth={2}
          borderColor={{ from: 'color' }}
          gridLevels={8}
          gridShape="circular"
          gridLabelOffset={25}
          enableDots={true}
          dotSize={10}
          dotColor={{ theme: 'background' }}
          dotBorderWidth={2}
          dotBorderColor={{ from: 'color', modifiers: [] }}
          enableDotLabel={true}
          dotLabel="value"
          dotLabelYOffset={-14}
          colors={{ scheme: 'set2' }}
          fillOpacity={0.5}
          blendMode="multiply"
          animate={true}
          isInteractive={true}
        />
      </div>
    </div>
  )
}

export default Radar;
