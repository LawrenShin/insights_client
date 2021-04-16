import React, {useMemo} from "react";
import { ResponsivePie } from '@nivo/pie';
import {keyTitle} from "../../helpers";
import {GenderColorsMap, RaceColorMap} from "../colorConstants";


interface Data {
  [key: string]: { name: string, percentage: number },
}
interface DataPiece {
  id: string,
  label: string,
  value: number,
  color?: string,
}

const reData = (data: Data): DataPiece[] =>
  Object.keys(data).map(
    (key): DataPiece => ({
      id: key,
      label: keyTitle(key),
      value: data[key].percentage,
      color: GenderColorsMap.get(key) || RaceColorMap.get(key),
    })
  );

interface Props {
  data: Data,
  height?: number,
}


const PieRating = ({ height, data }: Props) => {
  const expensiveResult = useMemo(() => reData(data), [data])

  return (
    <div>
      <div style={{position: 'relative', height: height || '100px', width: '100%'}}>
        <div style={{position: 'absolute', height: height || '100px', width: '100%'}}>
          <ResponsivePie
            data={expensiveResult}
            margin={{top: 20, right: 0, bottom: 20, left: 0}}
            innerRadius={0.7}
            colors={d => d.data.color || 'red'}
            enableRadialLabels={false}
            enableSliceLabels={false}
          />
        </div>
      </div>
      <div>
        <span>flsdjhbfsk</span>
        <span>flsdjhbfsk</span>
        <span>flsdjhbfsk</span>
        <span>flsdjhbfsk</span>
        <span>flsdjhbfsk</span>
      </div>
    </div>
  )
}

export default PieRating