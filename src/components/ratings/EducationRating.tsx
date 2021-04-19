import React from 'react';
// @ts-ignore
import dummyBubbles from "./dummyBubbles";
import BubbleChart from "../charts/bubbleChart";
import RatingWrapper from "./RatingWrapper";

interface DataItem {
  name: string,
  percentage: number,
}
interface Props {
  title: string,
  data: DataItem[],
  height?: string
}

const EducationRating = ({title, data, height}: Props) => {

  return (
    <RatingWrapper
      title={title}
      sm={4}
    >
      <BubbleChart
        data={data}
        height={height || '150px'}
      />
    </RatingWrapper>
  )
}

export default EducationRating;
