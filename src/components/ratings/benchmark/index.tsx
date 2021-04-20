import React from 'react';
import TriangleSvg from "../../TriangleSvg";
import {useStyles} from "./useStyles";
import _ from "lodash";

type BenchmarkItem = {
  quality: string,
  rating: string,
  score: number,
  strength: number,
}
interface Data {
  companyRating: BenchmarkItem,
  peerRating: BenchmarkItem,
}
interface Props {
  data: Data,
}

const Benchmark = ({ data }: Props) => {
  const {
    containerCol,
    containerRow,
    indicator,
    progressBar,
    barParts,
    part, partA, partB, partC,
    paleFont,
    triangle,
    gap5,
    mrgTop50,
    circle,
    localLegendItem,
  } = useStyles();


  const renderIndicator = (percentage: number, isCompany?: boolean) => <div
    style={{ left: `${percentage}%` }}
    className={`${indicator}`}
  >
    {isCompany ?
      <TriangleSvg styles={triangle} />
      :
      <div className={circle}></div>}
  </div>;

  const renderRatingBar = (name: string) => <div
    className={`
      ${part} 
      ${name === 'C' ? 
        partC : name === 'B' ? 
          partB : partA}
    `}
  >
    {name === 'C' && <span>D</span>}
    {[1, 2, 3].map((n) => <span key={Math.random()}>{_.times(n, () => name)}</span>)}
  </div>

  return (<div className={`${containerCol} ${gap5}`}>
    <div className={containerRow}>
      {renderIndicator(data.peerRating.score)}
      {renderIndicator(data.companyRating.score, true)}
    </div>

    <div className={progressBar}>
      <div className={`${barParts} ${paleFont}`}>
        {renderRatingBar('C')}
        {renderRatingBar('B')}
        {renderRatingBar('A')}
      </div>
    </div>
    <div className={`${mrgTop50} ${containerRow} ${gap5}`}>
      <div className={localLegendItem}>
        <TriangleSvg styles={triangle} />
        <span className={paleFont}>Company</span>
      </div>
      <div className={localLegendItem}>
        <div className={circle}> </div>
        <span className={paleFont}>Peer Group</span>
      </div>
    </div>
  </div>);
}

export default Benchmark;