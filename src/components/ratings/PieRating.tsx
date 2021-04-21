import React, {useMemo} from "react";
import { ResponsivePie } from '@nivo/pie';
import {keyTitle} from "../../helpers";
import {GenderColorsMap, RaceColorMap} from "../colorConstants";
import Legend from "../charts/Legend";
import ChartWrapper from "./ChartWrapper";


interface Data {
  [key: string]: { name: string, percentage: number },
}
interface Props {
  data: Data,
  height?: string,
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


const PieRating = ({ height, data }: Props) => {
  const expensiveResult = useMemo(() => reData(data), [data])

  return (
    <div>
      <ChartWrapper height={height}>
          <ResponsivePie
            data={expensiveResult}
            margin={{top: 20, right: 0, bottom: 20, left: 0}}
            innerRadius={0.7}
            colors={d => d.data.color || 'red'}
            enableRadialLabels={false}
            enableSliceLabels={false}
          />
      </ChartWrapper>
      <Legend legend={data} />
    </div>
  )
}

export default PieRating