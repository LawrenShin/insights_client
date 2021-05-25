import React from 'react';
import TriangleSvg from "../../TriangleSvg";
import {useStyles} from "./useStyles";
import _ from "lodash";
import {Grid} from "@material-ui/core";
import {renderSingleProp} from "../../../pages/details/helpers";
import EssentialRadialRating from "../EssentialRating";

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
  data: any,
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
    marginChart,
  } = useStyles();

  const tab = localStorage.getItem('tab') || 'company';
  const isCompanyTab = tab === 'company';

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
    {(name === 'C' && tab !== 'industry') && <span>D</span>}
    {[1, 2, 3].map((n) => <span key={Math.random()}>
      {tab === 'company' && _.times(n, () => name)}
    </span>)}
  </div>

  return (<div className={`${containerCol} ${gap5} ${marginChart}`}>

    <div className={containerRow}>
      {(data?.peerRating && isCompanyTab) && renderIndicator(data.peerRating.score)}
      {(data?.companyRating && isCompanyTab) && renderIndicator(data.companyRating.score, true)}
      {!isCompanyTab && renderIndicator(data.score, true)}
    </div>

    <div className={progressBar}>
      <div className={`${barParts} ${paleFont}`}>
        {renderRatingBar('C')}
        {renderRatingBar('B')}
        {renderRatingBar('A')}
      </div>
      {(data.score && data.score !== 100) && <div style={{
        position: 'relative',
      }}>
        <span style={{
          left: `${data.score - 2}%`,
          position: 'relative'
        }}>{data.score}</span>
      </div>}
      {!isCompanyTab && <Grid
        container
        justify={data.score >= 5 ? "space-between" : 'flex-end'}
        style={{ marginTop: (data.score && data.score !== 100) ? '-22px' : 0 }}
      >
        {data.score >= 5 && <Grid item ><span>0</span></Grid>}
        {data.score <= 95 && <Grid item ><span>100</span></Grid>}
      </Grid>}
    </div>
    {isCompanyTab && <div className={`${mrgTop50} ${containerRow} ${gap5}`}>
      <div className={localLegendItem}>
        <TriangleSvg styles={triangle}/>
        <span className={paleFont}>Company</span>
      </div>
      <div className={localLegendItem}>
        <div className={circle}></div>
        <span className={paleFont}>Peer Group</span>
      </div>
    </div>}
  </div>);
}

export default Benchmark;